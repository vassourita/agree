import Router from 'next/router'
import Head from 'next/head'
import { FormEvent, useContext, useState } from 'react'
import { GetServerSideProps } from 'next'
import { parseCookies } from 'nookies'
import { AuthContext } from '../../logic/contexts/AuthContext'
import { Header } from '../../presentation/components/Header'

export default function Register() {
  const auth = useContext(AuthContext)

  const [email, setEmail] = useState('')
  const [displayName, setDisplayName] = useState('')
  const [password, setPassword] = useState('')
  const [passwordConfirmation, setPasswordConfirmation] = useState('')

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    var ok = await auth.register(email, displayName, password, passwordConfirmation)
    if (ok) {
      Router.push('/')
    } else {
      alert('Error')
    }
  }

  return (
    <div>
      <Head>
        <title>Home - Agree</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <Header />

      <hr />

      <main>
        <h2>Register</h2>
        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="username">UserName</label>
            <br />
            <input type="text" id="username" value={displayName} onChange={e => setDisplayName(e.target.value)} />
          </div>
          <div>
            <label htmlFor="email">Email</label>
            <br />
            <input type="text" id="email" value={email} onChange={e => setEmail(e.target.value)} />
          </div>
          <div>
            <label htmlFor="password">Password</label>
            <br />
            <input type="password" id="password" value={password} onChange={e => setPassword(e.target.value)} />
          </div>
          <div>
            <label htmlFor="password-confirmation">Password Confirmation</label>
            <br />
            <input type="password" id="password-confirmation" value={passwordConfirmation} onChange={e => setPasswordConfirmation(e.target.value)} />
          </div>
          <br />
          <div>
            <button>Register</button>
          </div>
        </form>
      </main>
    </div>
  )
}

export const getServerSideProps: GetServerSideProps = async (ctx) => {
  const { ['agreeaccord_accesstoken']: token } = parseCookies(ctx)

  if (token) {
    return {
      redirect: {
        destination: '/dashboard',
        permanent: false,
      }
    }
  }

  return {
    props: {}
  }
}