/**
 * Cookie utilities
 * @class Cookie
 * @namespace util
 * @module util
 */

const doc = document;

/**
 * Set a cookie item
 * @method set
 * @static
 * @param {String} name Cookie name
 * @param {*} value Cookie value
 * @param {Date} [expires=sessionTime] The expire time of cookie (单位：毫秒)
 * @chainable
 * @example
 *    setCookie('user', 'rainszhang', '', '' 86400000);
 */
export function setCookie(name, value, expires) {
  let tempExpires;

  if (expires) {
    tempExpires = new Date(Date.now() + expires).toGMTString();
    tempExpires = `; expires=${tempExpires}`;
  } else {
    tempExpires = '';
  }

  const tempCookie = `${name}=${escape(value)}${tempExpires}`;

  // Ensure the cookie's size is under the limitation
  if (tempCookie.length < 4096) {
    doc.cookie = tempCookie;
  }
}

/**
 * Get value of a cookie item
 * @method get
 * @static
 * @param {String} name Cookie name
 * @return {String|Null}
 * @example
 *    getCookie('user');
 */
export function getCookie(name) {
  const result = doc.cookie.match(new RegExp(`(^| )${name}=([^;]*)(;|$)`));

  if (result != null) {
    return unescape(result[2]);
  }

  return null;
}
/**
 * Delete a cookie item
 * @method del
 * @static
 * @param {String} name Cookie name
 * @chainable
 * @example
 *    delCookie('user');
 */
export function delCookie(name) {
  if (getCookie(name)) {
    doc.cookie = `${name}=;expires=Thu, 01-Jan-1970 00:00:01 GMT`;
  }
}

export default {
  set: setCookie,
  get: getCookie,
  del: delCookie,
};
