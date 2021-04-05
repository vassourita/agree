import { createContext, ReactNode, useEffect, useState } from 'react'
import { useHistory } from 'react-router'
import { Account } from '../models/Account'
import { ICache } from '../services/ICache'
import { IHttpClient } from '../services/IHttpClient'

export type AuthContextProps = {
  isAuthenticated: boolean
  account?: Account
  accessToken?: string
  refreshToken?: string
  login(email: string, password: string): void
  logout(): void
}

export const AuthContext = createContext<AuthContextProps>({} as AuthContextProps)

export type AuthProviderProps = {
  httpClient: IHttpClient
  cache: ICache
  children: ReactNode
}

const ACCESS_TOKEN_KEY = '@agree/access-token'
const REFRESH_TOKEN_KEY = '@agree/refresh-token'

export function AuthProvider ({ httpClient, cache, children }: AuthProviderProps): JSX.Element {
  const [account, setAccount] = useState<Account>()
  const [accessToken, setAccessToken] = useState<string>(cache.get<string>(ACCESS_TOKEN_KEY) || '')
  const [refreshToken, setRefreshToken] = useState<string>(cache.get<string>(REFRESH_TOKEN_KEY) || '')

  const history = useHistory()

  useEffect(() => {
    cache.set(ACCESS_TOKEN_KEY, accessToken)
  }, [accessToken])

  useEffect(() => {
    cache.set(REFRESH_TOKEN_KEY, refreshToken)
  }, [refreshToken])

  function login (email: string, password: string) {
    httpClient.request({
      method: 'post',
      body: {
        email,
        password,
        grantType: 'password'
      },
      url: 'http://localhost:5000/api/accounts/login'
    }).then(response => {
      setRefreshToken(response.body.refreshToken)
      setAccessToken(response.body.accessToken)
      setAccount(response.body.account)
      history.push('/')
    }).catch(error => {
      console.log(error)
    })
  }

  function logout () {
    cache.delete(ACCESS_TOKEN_KEY)
    cache.delete(REFRESH_TOKEN_KEY)
    history.push('/login')
  }

  return (
    <AuthContext.Provider value={{ account, isAuthenticated: !!account, login, logout, accessToken, refreshToken }}>
      {children}
    </AuthContext.Provider>
  )
}
