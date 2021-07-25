import Link from 'next/link'
import { useContext } from 'react'
import { AuthContext } from '../contexts/AuthContext'

export function Header() {
  const auth = useContext(AuthContext)

  return (
    <header>
      <h1>
        <Link href="/">Agree</Link>
      </h1>
      {auth.isReady ? (
        <nav>
          {auth.isAuthenticated && auth.user ? (
            <>
              <Link href="/profile">{auth.user.nameTag}</Link>
              {' - '}
              <button onClick={() => auth.logout()}>Sair</button>
            </>
          ) : (
            <>
              <Link href="/login">Login</Link>
              {' - '}
              <Link href="/register">Register</Link>
            </>
          )}
        </nav>
      ) : (
        <div>Loading...</div>
      )}
    </header>
  )
}