#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>
#define size 10
int isSeparator(char c) {
	if (c == '!' || c == '?' || c == '.' || c == ',' || c == ' ')
		return 1;

	return 0;
}

/*
char* next_word(char* sentence, char word[]) {
	int i = 0;
	while (*sentence != NULL) {
		if (isSeparator(*sentence)) {
			printf("%s", word);
			return *sentence+1;
		}
		word[i] = *sentence;
		sentence += 1;
		i++;
	}
}
*/

char* next_word(char* sentence, char word[]) {
	int i;
	for (i = 0;i < strlen(sentence) && !isSeparatorChar(sentence[i]);i++)
		word[i] = sentence[i];
	word[i] = '\0';
	while (i < strlen(sentence) && isSeparatorChar(sentence[i]))
		i++;
	if (i == strlen(sentence))
		return NULL;
	printf("%s", word);
	return sentence + i;
}


int countWordOccurences(char* query, char* sentence) {
	int count = 0;
	char Word[size];

	sentence = next_word(sentence, query);

	printf("%d", count);
}
	void main() {
		char* sentence = "hey!what's up?bitch";
		char word[size] = "";
		char query[size] = "hey";
		 next_word(sentence,word);
		 countWordOccurences(query,sentence);
		
		 
	}