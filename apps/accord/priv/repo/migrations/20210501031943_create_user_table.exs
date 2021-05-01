defmodule Accord.Repo.Migrations.CreateUserTable do
  use Ecto.Migration

  def change do
    create table :users do
      add :email, :string, size: 100
      add :email_verified, :boolean
      add :user_name, :string, size: 40
      add :tag, :integer
      add :password_hash, :string, size: 255
      add :avatar_url, :string, size: 255

      timestamps()
    end

    create unique_index(:users, [:email])
  end
end
