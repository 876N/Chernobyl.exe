bits 16
org 0x7C00

start:
    cli
    xor ax, ax
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x7C00
    sti

    mov ax, 0x0003
    int 0x10

    mov ax, 0x0600
    mov bh, 0x14
    mov cx, 0x0000
    mov dx, 0x184F
    int 0x10

    mov ah, 0x02
    mov bh, 0x00
    mov dh, 11
    mov dl, 32
    int 0x10

    mov si, message
    mov ah, 0x0E
    mov bh, 0x00

print_loop:
    lodsb
    cmp al, 0
    je start_color_cycle
    cmp al, 10
    jne print_char
    inc dh
    mov dl, 32
    mov ah, 0x02
    int 0x10
    mov ah, 0x0E
    jmp print_loop

print_char:
    int 0x10
    jmp print_loop

start_color_cycle:
    mov byte [current_bg], 1

color_loop:
    mov al, [current_bg]
    mov bl, 15
    sub bl, al
    shl al, 4
    or al, bl

    push ax
    mov ax, 0xB800
    mov es, ax
    pop ax
    mov di, 1
    mov cx, 2000

fill_attr:
    mov [es:di], al
    add di, 2
    loop fill_attr

    call delay

    inc byte [current_bg]
    cmp byte [current_bg], 14
    jbe color_loop
    mov byte [current_bg], 1
    jmp color_loop

delay:
    push cx
    push dx
    mov cx, 0xFFFF
.d1:
    mov dx, 0x0FFF
.d2:
    dec dx
    jnz .d2
    loop .d1
    pop dx
    pop cx
    ret

message:
    db " Checkmate!!!",10,"www.abolhb.com",0

current_bg:  db 0

times 510-($-$$) db 0
dw 0xAA55