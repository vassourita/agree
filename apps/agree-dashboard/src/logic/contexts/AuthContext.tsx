import { createContext, useState } from 'react'
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
}

export function AuthProvider ({ httpClient, cache }: AuthProviderProps): JSX.Element {
  const [account, setAccount] = useState<Account>()
  const [accessToken, setAccessToken] = useState<string>('')
  const [refreshToken, setRefreshToken] = useState<string>('')

  const history = useHistory()

  function login (email: string, password: string) {
    httpClient.request({
      method: 'post',
      body: {
        email,
        password,
        grantType: 'password'
      },
      url: ''
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
    cache.delete('')
    history.push('/login')
  }

  return (
    <AuthContext.Provider value={{ account, isAuthenticated: !!account, login, logout, accessToken, refreshToken }}>

    </AuthContext.Provider>
  )
}
