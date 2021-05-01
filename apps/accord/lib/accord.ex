defmodule Accord do
  alias Accord.Users.Create, as: UsersCreate

  defdelegate create_user(params), to: UsersCreate, as: :call
end
