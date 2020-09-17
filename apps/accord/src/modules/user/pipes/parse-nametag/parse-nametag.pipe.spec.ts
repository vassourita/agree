import { ParseNametagPipe } from './parse-nametag.pipe'

describe('ParseNametagPipe', () => {
  let sut: ParseNametagPipe

  beforeEach(() => {
    sut = new ParseNametagPipe()
  })

  it('should be defined', () => {
    expect(sut).toBeDefined()
    expect(sut.transform).toBeDefined()
  })

  it('should divide nametag into name and tag', async () => {
    expect(await sut.transform('Vassoura#8230')).toEqual(['Vassoura', 8230])
  })

  it('should throw if sent nametag is not a string', async () => {
    await expect(() => sut.transform(true)).rejects.toThrow('Invalid nametag')
    await expect(() => sut.transform(1)).rejects.toThrow('Invalid nametag')
    await expect(() => sut.transform(null)).rejects.toThrow('Invalid nametag')
  })

  it('should throw if sent nametag has wrong format (not separated by #)', async () => {
    await expect(() => sut.transform('Vassoura8230')).rejects.toThrow('Nametag malformatted')
    await expect(() => sut.transform('Vassoura-8230')).rejects.toThrow('Nametag malformatted')
    await expect(() => sut.transform('Vassoura 8230')).rejects.toThrow('Nametag malformatted')
  })

  it('should throw if second part of nametag is not a number', async () => {
    await expect(() => sut.transform('Vassoura#onetwo')).rejects.toThrow('Invalid tag number')
  })
})
