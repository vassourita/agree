import { User } from "./User";

export type FriendshipRequest = {
  from: User,
  to: User,
  accepted: boolean,
}