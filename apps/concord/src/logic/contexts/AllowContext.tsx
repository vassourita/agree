import { useToast } from '@chakra-ui/toast'
import { createContext, ReactNode, useEffect, useState } from 'react'
import { useCookies } from 'react-cookie'
import { useHistory, useLocation } from 'react-router'
import { useI18n } from '../../presentation/hooks/useI18n'
import { RegisterInput } from '../../validation/RegisterValidator'
import { Account } from '../models/Account'
import { ICache } from '../services/ICache'
import { HttpStatusCode, IHttpClient } from '../services/IHttpClient'
import { ILogger } from '../services/ILogger'

export type AllowContextProps = {
  isAuthenticated: boolean
  account: Account | null
  resendConfirmationMail(): Promise<void>
  login(email: string, password: string): Promise<string[]>
  logout(): void
  register(input: RegisterInput): Promise<string[]>
}

export const AllowContext = createContext<AllowContextProps>({} as AllowContextProps)

export type AllowProviderProps = {
  httpClient: IHttpClient
  cache: ICache
  logger: ILogger
  children: ReactNode
}

const accountCookie = 'agreeconcord_account'

export function AllowProvider ({ httpClient, cache: _cache, children, logger: _logger }: AllowProviderProps): JSX.Element {
  const [account, setAccount] = useState<Account | null>(null)
  const [cookies, setCookie, removeCookie] = useCookies([accountCookie])

  const { t } = useI18n()

  const toast = useToast()
  const history = useHistory()
  const location = useLocation()

  async function me (): Promise<Account | null> {
    const response = await httpClient.request({
      method: 'get',
      url: `${process.env.REACT_APP_ALLOW_URL}/accounts/@me`
    })

    if (response.statusCode !== HttpStatusCode.OK) {
      setAccount(null)
      return null
    }

    const { user } = response.body
    setCookie(accountCookie, user)
    setAccount(user)
    return user
  }

  async function login (email: string, password: string): Promise<string[]> {
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
      const user = (await me() as Account)
      toast({
        title: t`Welcome, ${user?.userName}#${user?.tag}!`,
        isClosable: true,
        status: 'success'
      })
      history.push('/home')
      return []
    } else {
      toast({
        title: t`Email or password incorrect`,
        isClosable: true,
        status: 'error'
      })
      return []
    }
  }

  async function register (input: RegisterInput): Promise<string[]> {
    const response = await httpClient.request({
      method: 'post',
      body: {
        email: input.email,
        userName: input.userName,
        password: input.password,
        confirmPassword: input.confirmPassword
      },
      url: `${process.env.REACT_APP_ALLOW_URL}/accounts/register`
    })

    if (response.statusCode === HttpStatusCode.OK || response.statusCode === HttpStatusCode.CREATED) {
      const user = (await me() as Account)
      toast({
        title: t`Welcome, ${user?.userName}#${user?.tag}!`,
        description: t`We sent a confirmation mail to ${input.email}`,
        isClosable: true,
        status: 'success'
      })
      history.push('/home')
      return []
    } else if (response.body?.errors?.some((e: any) => e.code === 'DuplicateEmail')) {
      return [
        'Email has already been taken'
      ]
    } else {
      toast({
        title: t`An unexpected error ocurred`,
        description: t`Please try again later`,
        isClosable: true,
        status: 'error'
      })
      _logger.info(response)
    }
    return []
  }

  async function logout () {
    const response = await httpClient.request({
      method: 'post',
      url: `${process.env.REACT_APP_ALLOW_URL}/accounts/logout`
    })
    if (response.statusCode === HttpStatusCode.NOCONTENT) {
      setAccount(null)
      removeCookie(accountCookie)
      toast({
        title: t`Bye bye...`,
        isClosable: true,
        status: 'info'
      })
      history.push('/login')
    }
  }

  async function resendConfirmationMail () {
    if (!account?.email) {
      return
    }
    const response = await httpClient.request({
      method: 'post',
      url: `${process.env.REACT_APP_ALLOW_URL}/accounts/ResendConfirmationEmail`
    })
    if (response.statusCode === HttpStatusCode.OK) {
      toast({
        title: 'OkiDoki!',
        description: t`We sent a confirmation mail to ${account.email}`,
        isClosable: true,
        status: 'success'
      })
    } else {
      toast({
        title: t`An unexpected error ocurred`,
        description: t`Please try again later`,
        isClosable: true,
        status: 'error'
      })
    }
  }

  useEffect(() => {
    if (cookies[accountCookie]) {
      me()
    }
    const verifiedMailOk = new URLSearchParams(location.search).get('mailVerifiedOk')

    if (verifiedMailOk === 'true') {
      toast({
        title: t`Email verified succesfully`,
        isClosable: true,
        status: 'info'
      })
    } else if (verifiedMailOk === 'false') {
      toast({
        title: t`It was not possible to verify your email`,
        description: t`Please try again later`,
        isClosable: true,
        status: 'error'
      })
    }
  }, [])

  return (
    <AllowContext.Provider value={{ logout, account, isAuthenticated: !!(cookies[accountCookie]), login, register, resendConfirmationMail }}>
      {children}
    </AllowContext.Provider>
  )
}
