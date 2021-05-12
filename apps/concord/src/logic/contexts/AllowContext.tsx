import { useToast } from '@chakra-ui/toast'
import { createContext, ReactNode, useEffect, useState } from 'react'
import { useHistory, useLocation } from 'react-router'
import { useI18n } from '../../presentation/hooks/useI18n'
import { useSyncCacheState } from '../hooks/useSyncCacheState'
import { Account } from '../models/Account'
import { ICache } from '../services/ICache'
import { HttpStatusCode, IHttpClient } from '../services/IHttpClient'
import { ILogger } from '../services/ILogger'

export type AllowContextProps = {
  isAuthenticated: boolean
  account: Account | null
  accessToken: string | null
  login(email: string, password: string): Promise<void>
  logout(): void
  register(userName: string, email: string, password: string): Promise<void>
}

export const AllowContext = createContext<AllowContextProps>({} as AllowContextProps)

export type AllowProviderProps = {
  httpClient: IHttpClient
  cache: ICache
  logger: ILogger
  children: ReactNode
}

const ACCESS_TOKEN_KEY = '@agree/access-token'

export function AllowProvider ({ httpClient, cache, children, logger }: AllowProviderProps): JSX.Element {
  const [accessToken, setAccessToken] = useSyncCacheState<string>(cache, ACCESS_TOKEN_KEY)
  const [account, setAccount] = useState<Account | null>(null)

  const { t } = useI18n()

  const toast = useToast()
  const history = useHistory()
  const location = useLocation()

  useEffect(() => {
    const verifiedEmail = new URLSearchParams(location.search).get('email_verified')
    if (verifiedEmail) {
      toast({
        title: t`Email verified succesfully`,
        description: t`Now you can login into Agree!`,
        isClosable: true,
        status: 'info'
      })
    }
  }, [])

  async function me (): Promise<Account | null> {
    const response = await httpClient.request({
      method: 'get',
      headers: {
        Authorization: `Bearer ${accessToken()}`
      },
      url: `${process.env.REACT_APP_ALLOW_URL}/accounts/@me`
    })

    if (response.statusCode !== HttpStatusCode.OK) {
      setAccessToken(null)
      setAccount(null)
      return null
    }

    const { user } = response.body
    setAccount(user)
    return user
  }

  async function login (email: string, password: string) {
    const response = await httpClient.request({
      method: 'post',
      body: {
        email,
        password,
        grantType: 'password'
      },
      url: `${process.env.REACT_APP_ALLOW_URL}/accounts/login`
    })

    if (response.statusCode === HttpStatusCode.OK) {
      setAccessToken(response.body.accessToken)
      await me()
      history.push('/')
    } else {
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
      url: `${process.env.REACT_APP_ALLOW_URL}/accounts/register`
    })

    if (response.statusCode === HttpStatusCode.OK || response.statusCode === HttpStatusCode.CREATED) {
      toast({
        title: t`${response.body.message}`,
        description: t`We sent a confirmation mail to ${email}.`,
        isClosable: true,
        status: 'success'
      })
      setAccessToken(response.body.accessToken)
      await me()
      history.push('/')
    } else {
      toast({
        title: t`${response.body.message}`,
        isClosable: true,
        status: 'error'
      })
    }
  }

  const tryAuthenticate = () =>
    accessToken() && me()
      .then(() => location.pathname === '/login' && history.push('/'))

  useEffect(() => {
    tryAuthenticate()
  }, [])

  useEffect(() => {
    tryAuthenticate()
  }, [accessToken()])

  useEffect(() => {
    if (account) {
      toast({
        title: t`Welcome, ${account?.userName}#${account?.tag}!`,
        isClosable: true,
        status: 'success'
      })
    }
  }, [account])

  function logout () {
    setAccessToken(null)
    history.push('/login')
  }

  return (
    <AllowContext.Provider value={{ accessToken: accessToken(), logout, account, isAuthenticated: !!(account), login, register }}>
      {children}
    </AllowContext.Provider>
  )
}
