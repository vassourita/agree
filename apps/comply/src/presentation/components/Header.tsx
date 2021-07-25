import Link from 'next/link'
import Router from 'next/router'
import { useContext } from 'react'
import { AuthContext } from '../../logic/contexts/AuthContext'

export function Header() {
  const auth = useContext(AuthContext)

  async function handleLogout() {
    const ok = await auth.logout()
    if (ok) {
      Router.push(`/account/login`)
    }
  }

  return (
    <header>
      <h1>
        <Link href="/">Agree</Link>
      </h1>
      {auth.isReady ? (
        <nav>
          {auth.isAuthenticated && auth.user ? (
            <>
              <Link href="/account/profile">{auth.user.nameTag}</Link>
              {' - '}
              <button onClick={handleLogout}>Sair</button>
            </>
          ) : (
            <>
              <Link href="/account/login">Login</Link>
              {' - '}
              <Link href="/account/register">Register</Link>
            </>
          )}
        </nav>
      ) : (
        <div>Loading...</div>
      )}
    </header>
  )
}