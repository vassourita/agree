import Router from 'next/router'
import Head from 'next/head'
import { FormEvent, useContext, useState } from 'react'
import { GetServerSideProps } from 'next'
import { parseCookies } from 'nookies'
import { AuthContext } from '../../logic/contexts/AuthContext'
import { Header } from '../../presentation/components/Header'

export default function Login() {
  const auth = useContext(AuthContext)

  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    var ok = await auth.login(email, password)
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
        <h2>Login</h2>
        <form onSubmit={handleSubmit}>
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
          <br />
          <div>
            <button>Login</button>
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