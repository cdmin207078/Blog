
export function assign(obj) {
  const args = [].slice.call(arguments, 0); // eslint-disable-line prefer-rest-params

  if (args.length === 0) {
    return obj;
  }

  const keys = Object.keys(obj);

  args.forEach((arg) => {
    let i = keys.length;

    while (i--) {
      if (typeof arg[keys[i]] !== 'undefined') {
        obj[keys[i]] = arg[keys[i]];
      }
    }
  });

  return obj;
}

export function extend(obj) {
  const args = [].slice.call(arguments, 0); // eslint-disable-line prefer-rest-params

  if (args.length === 0) {
    return obj;
  }

  args.forEach((arg) => {
    const keys = Object.keys(arg);
    let i = keys.length;

    while (i--) {
      obj[keys[i]] = arg[keys[i]];
    }
  });

  return obj;
}

export function find(arr, fn) {
  if (!Array.isArray(arr) || typeof fn !== 'function') {
    return null;
  }

  if (arr.find) {
    return arr.find(fn);
  }

  const len = arr.length;
  let matched = false;
  let i = 0;

  while (i < len) {
    if (fn.call(null, arr[i], i) !== false) {
      matched = true;
      break;
    }
    i++;
  }

  if (matched) {
    return arr[i];
  }

  return null;
}

export default {
  assign,
  extend,
  find,
};
