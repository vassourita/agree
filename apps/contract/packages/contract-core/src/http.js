import axios from 'axios'

export function createHttpClient(baseUrl) {
    return axios.create({
        baseURL: baseUrl,
        validateStatus: () => false
    })
}