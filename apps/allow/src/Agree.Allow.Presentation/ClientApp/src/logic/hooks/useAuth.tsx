import { Fragment, FunctionComponent, PropsWithChildren, useContext } from 'react'
import { AuthContext, AuthContextProps } from '../contexts/AuthContext'

type AuthHook = {
  AuthenticatedTemplate: FunctionComponent<PropsWithChildren<unknown>>
  UnauthenticatedTemplate: FunctionComponent<PropsWithChildren<unknown>>
} & AuthContextProps

export function useAuth (): AuthHook {
  const ctx = useContext(AuthContext)

  function AuthenticatedTemplate ({ children }: PropsWithChildren<unknown>): JSX.Element {
    return (
      <Fragment>
        {ctx.isAuthenticated && children}
      </Fragment>
    )
  }

  function UnauthenticatedTemplate ({ children }: PropsWithChildren<unknown>): JSX.Element {
    return (
      <Fragment>
        {!ctx.isAuthenticated && children}
      </Fragment>
    )
  }

  return {
    ...ctx,
    AuthenticatedTemplate,
    UnauthenticatedTemplate
  }
}
