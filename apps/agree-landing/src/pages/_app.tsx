import { AppProps } from 'next/app'
import Head from 'next/head'
import ReactTooltip from 'react-tooltip'

import '../styles/globals.css'

export default function MyApp ({ Component, pageProps }: AppProps) {
  return (
    <>
      <Head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Agree</title>
      </Head>
      <Component {...pageProps} />
      <ReactTooltip />
    </>
  )
}
