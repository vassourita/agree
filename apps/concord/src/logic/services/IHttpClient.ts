export type HttpMethod = 'post' | 'get' | 'put' | 'delete'

export enum HttpStatusCode {
  OK = 200,
  CREATED = 201,
  NOCONTENT = 204,
  BADREQUEST = 400,
  UNAUTHORIZED = 401,
  FORBIDDEN = 403,
  NOTFOUND = 404,
  SERVERERROR = 500
}

export type HttpRequest = {
  url: string
  method: HttpMethod
  body?: any
  headers?: any
}

export type HttpResponse<T = any> = {
  statusCode: HttpStatusCode
  body?: T
  headers?: any
}

export interface IHttpClient {
  request: <T = any>(data: HttpRequest) => Promise<HttpResponse<T>>
}
