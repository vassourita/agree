defmodule Accord.Repo.Migrations.CreateUsers do
  use Ecto.Migration

  def change do
    create table(:users, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :user_name, :string
      add :email, :string
      add :tag, :integer
      add :email_verified, :boolean, default: false, null: false
      add :avatar_url, :string
      add :password_hash, :string

      timestamps()
    end

    create unique_index(:users, [:email])
  end
end
