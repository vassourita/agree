import { GetServerSideProps } from "next";
import Head from "next/head";
import Link from "next/link";
import { parseCookies } from "nookies";
import { useContext } from "react";
import { FriendshipContext } from "../logic/contexts/FriendshipContext";
import { Header } from "../presentation/components/Header";

export default function Dashboard() {
  const friendship = useContext(FriendshipContext)

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
          <h4>Friends - {friendship.friends.length}</h4>
          <ul>
            {friendship.friends.map(friend => (
              <li key={friend.id}>
                <Link href={`/u/${friend.id}`}>{friend.nameTag}</Link>
              </li>
            ))}
          </ul>
        </div>

        <br />
        
        <div>
          <h4>Requests</h4>
          <div>
            <span>
              <Link href={`friendship-requests/new`}>New</Link>
            </span>
          </div>
          <br />
          <span>Received - {friendship.receivedRequests.length}</span>
          <ul>
            {friendship.receivedRequests.map(req => (
              <li key={req.from.id}>
                <span>
                  {req.from.nameTag}
                  <button onClick={() => friendship.acceptFriendRequest(req.from)}>Accept</button>
                  <button onClick={() => friendship.declineFriendRequest(req.from)}>Decline</button>
                </span>
              </li>
            ))}
          </ul>
          <span>Sent - {friendship.sentRequests.length}</span>
          <ul>
            {friendship.sentRequests.map(req => (
              <li key={req.to.id}>
                <span>
                  {req.to.nameTag}
                </span>
              </li>
            ))}
          </ul>
        </div>

        <br />

        <div>
          <h4>Servers</h4>
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
        destination: '/account/login',
        permanent: false,
      }
    }
  }

  return {
    props: {}
  }
}