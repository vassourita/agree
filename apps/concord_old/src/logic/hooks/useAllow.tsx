import { useContext } from 'react'
import { Redirect, Route, RouteProps } from 'react-router'
import { AllowContext, AllowContextProps } from '../contexts/AllowContext'

export function useAllow (): AllowContextProps {
  const ctx = useContext(AllowContext)

  return {
    ...ctx
  }
}

export function AuthenticatedRoute ({ component, ...rest }: RouteProps): JSX.Element {
  const ctx = useContext(AllowContext)
  const Component = component as any
  return (
    <Route {...rest} render={props => (
      ctx.isAuthenticated
        ? <Component {...props} />
        : <Redirect to="/login" />
    )} />
  )
}

export function UnauthenticatedRoute ({ component, ...rest }: RouteProps): JSX.Element {
  const ctx = useContext(AllowContext)
  const Component = component as any
  return (
    <Route {...rest} render={props => (
      !ctx.isAuthenticated
        ? <Component {...props} />
        : <Redirect to="/home" />
    )} />
  )
}
