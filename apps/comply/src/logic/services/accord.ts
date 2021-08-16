import axios from "axios";
import { parseCookies } from "nookies";

export function getAccordClient(ctx?: any) {
  const { 'nextauth.token': token } = parseCookies(ctx)

  const api = axios.create({
    baseURL: process.env.NEXT_PUBLIC_API_BASE_URL,
    withCredentials: true,
    validateStatus: () => true,
  })

  if (token) {
    api.defaults.headers['agreeaccord_accesstoken'] = `Bearer ${token}`;
  }

  return api;
}

export async function getLoggedAccountRequest(ctx?: any) {
  const response = await getAccordClient(ctx).get("/api/identity/accounts/@me")
  if (response.status === 200) {
    return response.data.user
  }
  return null
}

export async function getAccountByIdRequest(id: string, ctx?: any) {
  const response = await getAccordClient(ctx).get(`/api/identity/accounts/${id}`)
  if (response.status === 200) {
    return response.data.user
  }
  return null
}