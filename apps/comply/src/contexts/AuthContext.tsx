import React, { PropsWithChildren, useEffect, useState } from "react"
import { useContext } from "react";
import { accord, getLoggedAccount } from "../services/accord";

export type User = {
  id: string,
  tag: string,
  nameTag: string,
  displayName: string
}

type AuthContextData = {
  user?: User,
  isReady: boolean,
  isAuthenticated: boolean,
  login(email: string, password: string): Promise<boolean>,
  logout(): Promise<boolean>,
  register(email: string, displayName: string, password: string, passwordConfirmation: string): Promise<boolean>
}

export const AuthContext = React.createContext<AuthContextData>({} as AuthContextData)

export function AuthContextProvider(props: PropsWithChildren<any>) {
  const [user, setUser] = useState<User>()
  const [isReady, setIsReady] = useState(false)
  const [isAuthenticated, setIsAuthenticated] = useState(false)

  async function login(email: string, password: string) {
    const response = await accord.post<{user:User}>("/api/identity/sessions", {
      email, password
    })
    if (response.status === 204) {
      await me()
      return true
    }
    setUser(undefined)
    setIsAuthenticated(false)
    return false
  }

  async function logout() {
    const response = await accord.delete("/api/identity/sessions")
    if (response.status === 204) {
      setUser(undefined)
      setIsAuthenticated(false)
      return true
    }
    return false
  }

  async function register(email: string, displayName: string, password: string, passwordConfirmation: string) {
    const response = await accord.post("/api/identity/accounts", {
      email,
      password,
      displayName,
      passwordConfirmation,
    })
    if (response.status === 201) {
      setUser(response.data.user)
      setIsAuthenticated(true)
      return true
    }
    setUser(undefined)
    setIsAuthenticated(false)
    return false
  }

  async function me() {
    const user = await getLoggedAccount()
    if (!user) {
      setUser(undefined)
      setIsAuthenticated(false)
      await logout()
      return null
    }
    setUser(user)
    setIsAuthenticated(true)
    return user
  }

  useEffect(() => {
    me().then(() => setIsReady(true))
  }, [])


  return (
    <AuthContext.Provider value={{ user, isReady, isAuthenticated, login, logout, register }}>
      {props.children}
    </AuthContext.Provider>
  );
}