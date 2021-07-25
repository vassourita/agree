import { GetServerSideProps } from 'next'
import Head from 'next/head'
import { parseCookies } from 'nookies'
import { Header } from '../components/Header'

export default function Index() {
  return (
    <div>
      <Head>
        <title>Home - Agree</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <Header />

      <hr />

      <main>
        <h2>Home</h2>
      </main>
    </div>
  )
}