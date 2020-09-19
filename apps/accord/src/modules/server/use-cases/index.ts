import { DecodeInviteTokenUseCase } from './invite/decode-invite-token/decode-invite-token.use-case'
import { SignInviteTokenUseCase } from './invite/sign-invite-token/sign-invite-token.use-case'
import { AddMemberToServerUseCase } from './member/add-member-to-server/add-member-to-server.use-case'
import { FindMembersFromServerUseCase } from './member/find-members-from-server/find-members-from-server.use-case'
import { RemoveMemberUseCase } from './member/remove-member/remove-member.use-case'
import { CreateServerUseCase } from './server/create-server/create-server.use-case'
import { DeleteServerUseCase } from './server/delete-server/delete-server.use-case'
import { FindServerByIdUseCase } from './server/find-server-by-id/find-server-by-id.use-case'
import { FindServersByOwnerUseCase } from './server/find-servers-by-owner/find-servers-by-owner.use-case'
import { ListServersUseCase } from './server/list-servers/list-servers.use-case'
import { UpdateServerUseCase } from './server/update-server/update-server.use-case'

export const useCases = [
  AddMemberToServerUseCase,
  CreateServerUseCase,
  DecodeInviteTokenUseCase,
  FindServerByIdUseCase,
  FindServersByOwnerUseCase,
  ListServersUseCase,
  SignInviteTokenUseCase,
  UpdateServerUseCase,
  DeleteServerUseCase,
  FindMembersFromServerUseCase,
  RemoveMemberUseCase
]
