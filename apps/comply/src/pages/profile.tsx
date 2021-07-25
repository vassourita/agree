import { GetServerSideProps } from "next";
import Head from "next/head";
import { parseCookies } from "nookies";
import { useContext } from "react";
import { Header } from "../components/Header";
import { AuthContext } from "../contexts/AuthContext";

export default function Profile() {
  const { user } = useContext(AuthContext);

  return (
    <div>
      <Head>
        <title>Home - Agree</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <Header />

      <hr />

      <main>
        <h2>Profile</h2>
        <div>
          <p>
            <strong>Id:</strong>{user?.id}
            <br />
            <strong>Name:</strong>{user?.displayName}
            <br />
            <strong>Tag:</strong>{user?.tag}
            <br />
            <strong>Email:</strong>{user?.email}
          </p>
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