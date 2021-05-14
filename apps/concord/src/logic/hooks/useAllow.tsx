import { FunctionComponent, useContext } from 'react'
import { Redirect, Route, RouteProps } from 'react-router'
import { AllowContext, AllowContextProps } from '../contexts/AllowContext'

type AuthHook = {
  AuthenticatedRoute: FunctionComponent<RouteProps>
  UnauthenticatedRoute: FunctionComponent<RouteProps>
} & AllowContextProps

export function useAllow (): AuthHook {
  const ctx = useContext(AllowContext)

  function AuthenticatedRoute ({ component, ...rest }: RouteProps): JSX.Element {
    const Component = component as any
    return (
      <Route {...rest} render={props => (
        ctx.isAuthenticated
          ? <Component {...props} />
          : <Redirect to="/login" />
      )} />
    )
  }

  function UnauthenticatedRoute ({ component, ...rest }: RouteProps): JSX.Element {
    const Component = component as any
    return (
      <Route {...rest} render={props => (
        !ctx.isAuthenticated
          ? <Component {...props} />
          : <Redirect to="/home" />
      )} />
    )
  }

  return {
    ...ctx,
    AuthenticatedRoute,
    UnauthenticatedRoute
  }
}
