function set(key, data) {
  let json;

  try {
    json = JSON.stringify(data);
  } catch (ex) {
    json = '';
  }

  return localStorage.setItem(key, json);
}

function get(key) {
  let data;

  try {
    data = JSON.parse(localStorage.getItem(key));
  } catch (ex) {
    data = null;
  }

  return data;
}

function remove(key) {
  return localStorage.removeItem(key);
}

function clear() {
  return localStorage.clear();
}

export default {
  set,
  get,
  remove,
  clear,
};
