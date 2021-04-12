
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
void main() {
	char hexnum;
	int checkdig, count = 0,exp=1,num;

	printf("enter a riversed hexadecimal number:\n");
	scanf(" %c", &hexnum);
	while (hexnum != '\n') 
	{
		checkdig = hexnum;
		if (checkdig >= '0' && checkdig <= '9') {
			checkdig = checkdig - '0';
		}
		else if (checkdig >= 'A' && checkdig <= 'F') {
			checkdig = checkdig - 'A' + 10;
		}
		else if (checkdig >= 'a' && checkdig <= 'f') {
			checkdig = checkdig - 'a' + 10;
		}
		else {
			printf("Error! %c is not a valid digit in base 16\n", checkdig);
		}
		for (int i = 0;i < count;i++) {
			exp = exp * 16;
		}
	
		
		
	}	
		
	
	
	


}