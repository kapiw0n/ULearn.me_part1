public static MenuItem[] GenerateMenu()
{
    return new[] {
        new MenuItem {
            Caption = "File",
            HotKey = "F",
            Items = new[] {
                new MenuItem { Caption = "New", HotKey = "N", Items = null },
                new MenuItem { Caption = "Save", HotKey = "S", Items = null }
            }
        },
        new MenuItem {
            Caption = "Edit",
            HotKey = "E",
            Items = new[] {
                new MenuItem { Caption = "Copy", HotKey = "C", Items = null },
                new MenuItem { Caption = "Paste", HotKey = "V", Items = null }
            }
        }
    };
}