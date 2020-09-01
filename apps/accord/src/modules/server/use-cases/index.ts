import { AddMemberToServerUseCase } from './add-member-to-server/add-member-to-server.use-case'
import { CreateServerUseCase } from './create-server/create-server.use-case'
import { DecodeInviteTokenUseCase } from './decode-invite-token/decode-invite-token.use-case'
import { DeleteServerUseCase } from './delete-server/delete-server.use-case'
import { FindMembersFromServerUseCase } from './find-members-from-server/find-members-from-server.use-case'
import { FindServerByIdUseCase } from './find-server-by-id/find-server-by-id.use-case'
import { FindServersByOwnerUseCase } from './find-servers-by-owner/find-servers-by-owner.use-case'
import { ListServersUseCase } from './list-servers/list-servers.use-case'
import { RemoveMemberUseCase } from './remove-member/remove-member.use-case'
import { SignInviteTokenUseCase } from './sign-invite-token/sign-invite-token.use-case'
import { UpdateServerUseCase } from './update-server/update-server.use-case'

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
