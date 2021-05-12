import { Fragment, FunctionComponent, PropsWithChildren, useContext } from 'react'
import { AllowContext, AllowContextProps } from '../contexts/AllowContext'

type AuthHook = {
  AuthenticatedTemplate: FunctionComponent<PropsWithChildren<unknown>>
  UnauthenticatedTemplate: FunctionComponent<PropsWithChildren<unknown>>
} & AllowContextProps

export function useAllow (): AuthHook {
  const ctx = useContext(AllowContext)

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
