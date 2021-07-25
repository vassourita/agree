import { GetServerSideProps } from "next";
import Head from "next/head";
import Link from "next/link";
import { parseCookies } from "nookies";
import { useContext, useState } from "react";
import { FriendshipContext } from "../../logic/contexts/FriendshipContext";
import { ErrorList } from "../../logic/models/ErrorList";
import { FriendshipRequest } from "../../logic/models/FriendshipRequest";
import { ErrorAlert } from "../../presentation/components/ErrorAlert";
import { Header } from "../../presentation/components/Header";

export default function Friends() {
  const friendship = useContext(FriendshipContext)

  const [errors, setErrors] = useState<ErrorList>()

  async function accept(req: FriendshipRequest) {
    var errors = await friendship.acceptFriendRequest(req.from)
    if (errors) {
      setErrors(errors)
    }
  }

  async function decline(req: FriendshipRequest) {
    var errors = await friendship.declineFriendRequest(req.from)
    if (errors) {
      setErrors(errors)
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
        <h2>Friends</h2>

        <br />
        {errors && (
          <>
            <ErrorAlert errors={errors} />
            <br />
          </>
        )}

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
              <Link href={`/friends/new`}>New</Link>
            </span>
          </div>
          <br />
          <span>Received - {friendship.receivedRequests.length}</span>
          <ul>
            {friendship.receivedRequests.map((req, index) => (
              <li key={index}>
                <span>
                  {'>'} {req.from.nameTag}
                  <button onClick={() => accept(req)}>Accept</button>
                  <button onClick={() => decline(req)}>Decline</button>
                </span>
              </li>
            ))}
          </ul>
          <span>Sent - {friendship.sentRequests.length}</span>
          <ul>
            {friendship.sentRequests.map((req, index) => (
              <li key={index}>
                <span>
                  {'>'} {req.to.nameTag}
                </span>
              </li>
            ))}
          </ul>
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