import Head from 'next/head'
import NextImg from 'next/image'
import { GetServerSideProps } from 'next'
import { parseCookies } from 'nookies'

import LogoImg from '@presentation/assets/agreew.svg'
import { LoginForm } from '@presentation/components/account/LoginForm'
import { Box, Flex } from '@chakra-ui/react'

export default function Login() {
  return (
    <Flex bg={{ base: 'background', md: 'primary' }} w="full" minH="100vh" direction="column" align="center" justify="center">
      <Head>
        <title>Login - Agree</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

    <NextImg height="49.29px" width="180px" src={LogoImg} alt="Agree logo" />

    <LoginForm />

    </Flex>
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