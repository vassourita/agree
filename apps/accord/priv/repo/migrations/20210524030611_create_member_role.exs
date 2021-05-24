defmodule Accord.Repo.Migrations.CreateMemberRole do
  use Ecto.Migration

  def change do
    create table(:member_role, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :role_id, references(:role, on_delete: :delete_all, type: :binary_id)
      add :member_id, references(:member, on_delete: :delete_all, type: :string)

      timestamps()
    end

    create index(:member_role, [:role_id])
    create index(:member_role, [:member_id])
  end
end
