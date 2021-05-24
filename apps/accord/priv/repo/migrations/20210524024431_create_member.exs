defmodule Accord.Repo.Migrations.CreateMember do
  use Ecto.Migration

  def change do
    create table(:member, primary_key: false) do
      add :id, :string, primary_key: true
      add :server_id, references(:server, on_delete: :delete_all, type: :binary_id)

      timestamps()
    end

    create index(:member, [:server_id])
  end
end
