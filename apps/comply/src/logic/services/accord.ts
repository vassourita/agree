import axios from "axios";

export const accord = axios.create({
  baseURL: "http://localhost:5000",
  withCredentials: true,
  validateStatus: () => true,
})

export async function getLoggedAccount() {
  const response = await accord.get("/api/identity/accounts/@me")
  if (response.status === 200) {
    return response.data.user
  }
  return null
}