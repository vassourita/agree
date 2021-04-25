import { useToast } from '@chakra-ui/toast'
import { createContext, ReactNode, useEffect, useState } from 'react'
import { useHistory } from 'react-router'
import { useI18n } from '../../presentation/hooks/useI18n'
import { Account } from '../models/Account'
import { ICache } from '../services/ICache'
import { HttpResponse, HttpStatusCode, IHttpClient } from '../services/IHttpClient'
import { ILogger } from '../services/ILogger'

export type AuthContextProps = {
  isAuthenticated: boolean
  account: Account | null
  accessToken: string | null
  refreshToken: string | null
  refresh(): Promise<void>
  login(email: string, password: string): Promise<void>
  logout(): void
  register(userName: string, email: string, password: string): Promise<void>
}

export const AuthContext = createContext<AuthContextProps>({} as AuthContextProps)

export type AuthProviderProps = {
  httpClient: IHttpClient
  cache: ICache
  logger: ILogger
  children: ReactNode
}

const ACCESS_TOKEN_KEY = '@agree/access-token'
const REFRESH_TOKEN_KEY = '@agree/refresh-token'

export function AuthProvider ({ httpClient, cache, children, logger }: AuthProviderProps): JSX.Element {
  const [account, setAccount] = useState<Account | null>(null)
  const [accessToken, setAccessToken] = useState<string | null>(cache.get<string>(ACCESS_TOKEN_KEY)?.replace('"', '') || null)
  const [refreshToken, setRefreshToken] = useState<string | null>(cache.get<string>(REFRESH_TOKEN_KEY)?.replace('"', '') || null)

  const { t } = useI18n()

  const toast = useToast()
  const history = useHistory()

  function logout () {
    setRefreshToken(null)
    setAccessToken(null)
    setAccount(null)
    history.push('/login')
  }

  async function refresh (): Promise<void> {
    const response = await httpClient.request({
      method: 'post',
      body: {
        refreshToken,
        grantType: 'refresh_token'
      },
      url: `${process.env.REACT_APP_API_URL}/accounts/login`
    })

    if (response.statusCode === HttpStatusCode.OK) {
      setRefreshToken(response.body.refreshToken)
      setAccessToken(response.body.accessToken)
      logger.info(response.body)
    } else {
      setRefreshToken(null)
      setAccessToken(null)
      logger.info(response.body)
      toast({
        title: response.body.message,
        isClosable: true,
        status: 'error'
      })
    }
  }

  async function handleUnauthorized (response: HttpResponse) : Promise<boolean> {
    if (response.statusCode !== HttpStatusCode.UNAUTHORIZED) return true

    if (response.headers['WWW-Authenticate']) {
      await refresh()
      return true
    } else if (response.body?.message === 'Refresh token is expired') {
      toast({
        title: t`Your session expired`,
        description: t`Please login again`,
        isClosable: true,
        status: 'info'
      })
      logout()
    } else if (response.body?.message === 'Refresh token is invalid') {
      toast({
        title: t`An unexpected error occurred`,
        description: t`Please login again`,
        isClosable: true,
        status: 'info'
      })
      logout()
    }
    return false
  }

  async function me (): Promise<void> {
    const response = await httpClient.request({
      method: 'get',
      headers: {
        Authorization: `Bearer ${accessToken}`
      },
      url: `${process.env.REACT_APP_API_URL}/accounts/@me`
    })

    if (response.statusCode !== HttpStatusCode.OK && (await handleUnauthorized(response))) {
      return await me()
    }

    setAccount(response.body.user)
  }

  useEffect(() => {
    if (accessToken) {
      cache.set(ACCESS_TOKEN_KEY, accessToken)
    } else {
      cache.delete(ACCESS_TOKEN_KEY)
    }
  }, [accessToken])

  useEffect(() => {
    if (refreshToken) {
      cache.set(REFRESH_TOKEN_KEY, refreshToken)
    } else {
      cache.delete(REFRESH_TOKEN_KEY)
    }
  }, [refreshToken])

  useEffect(() => {
    if (account) {
      toast({
        title: t`Welcome, ${account?.userName}#${account?.tag}!`,
        isClosable: true,
        status: 'success'
      })
    }
  }, [account])

  async function login (email: string, password: string) {
    const response = await httpClient.request({
      method: 'post',
      body: {
        email,
        password,
        grantType: 'password'
      },
      url: `${process.env.REACT_APP_API_URL}/accounts/login`
    })

    if (response.statusCode === HttpStatusCode.OK) {
      setRefreshToken(response.body.refreshToken)
      setAccessToken(response.body.accessToken)
      await me()
      history.push('/')
    } else {
      logger.info(response)
      setRefreshToken(null)
      setAccessToken(null)
      toast({
        title: t`${response.body.message}`,
        isClosable: true,
        status: 'error'
      })
    }
  }

  async function register (userName: string, email: string, password: string) {
    const response = await httpClient.request({
      method: 'post',
      body: {
        email,
        password,
        userName
      },
      url: `${process.env.REACT_APP_API_URL}/accounts/register`
    })

    if (response.statusCode === HttpStatusCode.OK) {
      toast({
        title: t`${response.body.message}`,
        description: `Enviamos um email de confirmação para ${email}. Você precisa confirmar seu email antes de fazer login.`,
        isClosable: true,
        status: 'success'
      })
    } else {
      toast({
        title: t`${response.body.message}`,
        isClosable: true,
        status: 'error'
      })
    }
  }

  return (
    <AuthContext.Provider value={{ account, isAuthenticated: !!account, login, logout, accessToken, refreshToken, register, refresh }}>
      {children}
    </AuthContext.Provider>
  )
}
