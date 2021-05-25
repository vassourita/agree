defmodule Accord.Repo.Migrations.CreateMember do
  use Ecto.Migration

  def change do
    create table(:member, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :allow_user_id, :string, null: false
      add :server_id, references(:server, on_delete: :delete_all, type: :binary_id), null: false

      timestamps()
    end

    create unique_index(:member, [:allow_user_id, :server_id])
  end
end
