import 'whatwg-fetch'
import Promise from 'promise/lib/es6-extensions'

const Global = typeof self !== 'undefined' ? self : this

Global.Promise = Promise

export default Global.fetch.bind(Global)

export const Headers = Global.Headers
export const Request = Global.Request
export const Response = Global.Response
