/*  This file is part of Chummer5a.
 *
 *  Chummer5a is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Chummer5a is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Chummer5a.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  You can obtain the full source code for Chummer5a at
 *  https://github.com/chummer5a/chummer5a
 */
﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ChummerHub.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'passwordhash'
    public partial class passwordhash : Migration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'passwordhash'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'passwordhash.Up(MigrationBuilder)'
        protected override void Up(MigrationBuilder migrationBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'passwordhash.Up(MigrationBuilder)'
        {
            migrationBuilder.DropColumn(
                name: "GameMasterUsername",
                table: "SINnerGroups");
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'passwordhash.Down(MigrationBuilder)'
        protected override void Down(MigrationBuilder migrationBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'passwordhash.Down(MigrationBuilder)'
        {
            migrationBuilder.AddColumn<string>(
                name: "GameMasterUsername",
                table: "SINnerGroups",
                nullable: true);
        }
    }
}
