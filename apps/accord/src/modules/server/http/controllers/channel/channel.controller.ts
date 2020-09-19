import {
  Body,
  ClassSerializerInterceptor,
  Controller,
  Delete,
  Get,
  Param,
  Post,
  Put,
  UseGuards,
  UseInterceptors
} from '@nestjs/common'
import { InjectRepository } from '@nestjs/typeorm'

import { ChannelEntity } from '@modules/server/entities/channel.entity'
import { ServerMemberAuthGuard } from '@modules/server/guards/server-member/server-member.guard'
import { ServerOwnerAuthGuard } from '@modules/server/guards/server-owner/server-owner.guard'
import { JwtAuthGuard } from '@shared/guards/jwt/jwt.guard'
import { Repository } from 'typeorm'

import { ChannelDocs } from './docs/channel.docs'
import { CreateChannelDTO } from './dtos/create-channel.dto'
import { UpdateChannelDTO } from './dtos/update-channel.dto'

@Controller('/servers/:server_id/channels')
@UseInterceptors(ClassSerializerInterceptor)
@ChannelDocs()
export class ChannelController {
  constructor(@InjectRepository(ChannelEntity) private readonly channelRepository: Repository<ChannelEntity>) {}

  @Get('/')
  @UseGuards(JwtAuthGuard, new ServerMemberAuthGuard())
  async index(@Param('server_id') serverId: string) {
    return this.channelRepository.find({
      where: {
        serverId
      }
    })
  }

  @Post('/')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  async store(@Param('server_id') serverId: string, @Body() body: CreateChannelDTO) {
    const channel = this.channelRepository.create({
      name: body.name,
      category: body.category,
      serverId,
      type: body.type
    })

    await this.channelRepository.save(channel)
  }

  @Put('/:channel_id')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  async update(@Param('channel_id') channelId: string, @Body() body: UpdateChannelDTO) {
    const channel = await this.channelRepository.findOne(channelId)

    if (body.category !== channel.category) {
      channel.category = body.category
    }
    if (body.name !== channel.name) {
      channel.name = body.name
    }

    await this.channelRepository.save(channel)
  }

  @Delete('/:channel_id')
  @UseGuards(JwtAuthGuard, ServerOwnerAuthGuard)
  async destroy(@Param('channel_id') channelId: string) {
    await this.channelRepository.delete(channelId)
  }
}
