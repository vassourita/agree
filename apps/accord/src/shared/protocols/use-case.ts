export interface IUseCase<TInput, TOutput> {
  execute(data: TInput): Promise<TOutput>
}
