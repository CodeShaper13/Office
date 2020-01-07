using System;

[Flags]
public enum EnumInputBlock : int {

    NONE = 0,

    MOVE = 1,
    LOOK = 2,
    CHANGE_HELD = 4,

    ALL = MOVE | LOOK | CHANGE_HELD,
}
