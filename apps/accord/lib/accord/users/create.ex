defmodule Accord.Users.Create do
  alias Accord.{User, Repo}

  def call(params) do
    params
    |> User.changeset()
    |> Repo.insert()
  end
end
