
// 服务器地址
let serverUrl = '';

const noop = d => d;

/**
 * 设置服务器地址
 * @param {String} url 服务器地址
 */
export function setServerUrl(url) {
  if (typeof url === 'string') {
    serverUrl = url;
  }
}

export function registerFallbackSocket(ajaxGetter, delay, onMessage, onClose, onError) {
  if (typeof onMessage !== 'function') {
    throw new Error('onMessage参数必须为function');
  }

  if (typeof onError !== 'function') {
    onError = noop;
  }

  let timer;
  let closed = false;

  const clearTimer = () => {
    if (timer) {
      clearTimeout(timer);
      timer = null;
    }
  };

  const handler = () => {
    clearTimer();

    ajaxGetter().then((result) => {
      if (!closed) {
        timer = setTimeout(handler, delay);
        onMessage(result);
      }
    }, (err) => {
      if (!closed) {
        timer = setTimeout(handler, delay);
        onError(err);
      }
    });
  };

  handler();

  return () => {
    closed = true;
    clearTimer();
  };
}

export function registerSocket(url, onOpen, onMessage, onClose, onError) {
  if (!window.WebSocket) {
    console.wran('暂不支持 Websocket');
    return noop;
  }

  if (arguments.length === 2) {
    onMessage = onOpen;
    onOpen = null;
  }

  if (typeof onMessage !== 'function') {
    throw new Error('onMessage参数必须为function');
  }

  const ws = new WebSocket(`${serverUrl}${url}`);
  const typeofOnOpen = typeof onOpen;

  if (typeofOnOpen === 'function') {
    ws.onopen = () => {
      onOpen.call(null, ws);
    };
  } else if (typeofOnOpen === 'object' && onOpen) {
    ws.onopen = () => {
      ws.send(JSON.stringify(onOpen));
    };
  } else if (typeofOnOpen === 'string') {
    ws.onopen = () => {
      ws.send(onOpen);
    };
  }

  ws.onmessage = (evt) => {
    const data = JSON.parse(evt.data);

    if (data.event !== 'open') {
      onMessage.call(null, { data });
    }
  };

  ws.onclose = onClose;
  ws.onerror = onError;

  return () => {
    ws.close();
  };
}
