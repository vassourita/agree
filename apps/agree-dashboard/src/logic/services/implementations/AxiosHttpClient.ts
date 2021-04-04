import axios, { AxiosResponse } from 'axios'
import { HttpRequest, HttpResponse, IHttpClient } from '../IHttpClient'

export class AxiosHttpClient implements IHttpClient {
  async request <T = any> (data: HttpRequest): Promise<HttpResponse<T>> {
    let axiosResponse: AxiosResponse
    try {
      axiosResponse = await axios.request<T>({
        url: data.url,
        method: data.method,
        data: data.body,
        headers: data.headers
      })
    } catch (error) {
      axiosResponse = error.response
    }
    return {
      statusCode: axiosResponse.status,
      body: axiosResponse.data,
      headers: axiosResponse.headers
    }
  }
}
