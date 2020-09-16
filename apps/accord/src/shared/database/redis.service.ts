import { Injectable } from '@nestjs/common'

import { RedisService as RedisRootService } from 'nestjs-redis'
import { promisify } from 'util'

export enum RedisClientNames {
  BLACKLIST = '@blacklist',
  CACHE = '@cache'
}

@Injectable()
export class RedisService {
  constructor(private readonly redis: RedisRootService) {}

  async get<T = any>(client: RedisClientNames, key: string): Promise<T> {
    const result = await promisify<string, string>(this.redis.getClient(client).get).bind(this.redis.getClient(client))(
      key
    )

    return result && (JSON.parse(result) as T)
  }

  async set<T = any>(client: RedisClientNames, key: string, value: T): Promise<void> {
    await promisify<string, string>(this.redis.getClient(client).set).bind(this.redis.getClient(client))(key, value)
  }
}
