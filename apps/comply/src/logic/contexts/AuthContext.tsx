import React, { PropsWithChildren, useEffect, useState } from "react"
import { useContext } from "react";
import { ErrorList } from "../models/ErrorList";
import { FriendshipRequest } from "../models/FriendshipRequest";
import { User } from "../models/User";
import { getLoggedAccountRequest, getAccordClient } from "../services/accord";
import { SignalRService } from "../services/signalr";

type AuthContextData = {
  user?: User,
  isReady: boolean,
  isAuthenticated: boolean,
  login(email: string, password: string): Promise<ErrorList | null>,
  logout(): Promise<boolean>,
  register(email: string, displayName: string, password: string, passwordConfirmation: string): Promise<ErrorList | null>
}

const accord = getAccordClient();

export const AuthContext = React.createContext<AuthContextData>({} as AuthContextData)

export function AuthContextProvider(props: PropsWithChildren<any>) {
  const [user, setUser] = useState<User>()
  const [isReady, setIsReady] = useState(false)
  const [isAuthenticated, setIsAuthenticated] = useState(false)

  async function login(email: string, password: string) {
    const response = await accord.post("/api/identity/sessions", {
      email, password
    })
    if (response.status === 204) {
      await me()
      return null
    }
    setUser(undefined)
    setIsAuthenticated(false)
    return {
      'Account': ['Email or password are incorrect']
    } as ErrorList
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
      return null
    }
    setUser(undefined)
    setIsAuthenticated(false)
    return response.data.errors
  }

  async function me() {
    const user = await getLoggedAccountRequest()
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