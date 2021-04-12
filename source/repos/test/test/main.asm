.386
.model flat, stdcall
.stack 4096
ExitProcess PROTO, dwExitCode: DWORD

.data
	; define your variables here
.code

main PROC
	; write your assembly code here
		mov		ebx, 3
		mov		ecx, 5


	INVOKE ExitProcess, 0
main ENDP
END main