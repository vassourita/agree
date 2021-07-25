import { GetServerSideProps } from "next";
import Head from "next/head";
import { parseCookies } from "nookies";
import { Header } from "../components/Header";

export default function Dashboard() {
  return (
    <div>
      <Head>
        <title>Home - Agree</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <Header />

      <hr />

      <main>
        <h2>Dashboard</h2>

        <div>
          <h6>Friends</h6>
        </div>

        <div>
          <h6>Servers</h6>
        </div>
      </main>
    </div>
  );
}

export const getServerSideProps: GetServerSideProps = async (ctx) => {
  const { ['agreeaccord_accesstoken']: token } = parseCookies(ctx)

  if (!token) {
    return {
      redirect: {
        destination: '/login',
        permanent: false,
      }
    }
  }

  return {
    props: {}
  }
}