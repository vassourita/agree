import axios from "axios";

export const allow = axios.create({
  baseURL: "http://localhost:5000",
  withCredentials: true,
  validateStatus: () => true,
})

export async function getLoggedAccount() {
  const response = await allow.get("/api/identity/accounts/@me")
  if (response.status === 200) {
    return response.data.user
  }
  return null
}