#include <stdio.h>
#include "dictionary.h"

void showDictionary(Dictionary* dictionary) {
	printf("Size: %d\nDictionary: ", sizeOfDictionary(dictionary));
	printDictionary(dictionary);
	printf("\n");
}

void main() {
	Dictionary* dictionary = initDictionary();
	showDictionary(dictionary);

	putInDictionary(dictionary, 1, 10);
	showDictionary(dictionary);

	putInDictionary(dictionary, 1, 2);
	showDictionary(dictionary);

	removeFromDictionary(dictionary, 1);
	showDictionary(dictionary);

	removeFromDictionary(dictionary, 1);
	showDictionary(dictionary);

	printf("43: %d\n", getFromDictionary(dictionary, 43));
	showDictionary(dictionary);

	destroyDictionary(dictionary);

	int keys[] = { 7,-61,43,-12,14,97,0,12 };
	int values[] = { 10,2,-5,7,-9,10,10,10 };
	int size = sizeof(keys) / sizeof(int);

	dictionary = createDictionaryFromArrays(keys, values, size);
	showDictionary(dictionary);

	int value = getFromDictionary(dictionary, 43);
	printf("43: %d\n", value);
	showDictionary(dictionary);

	removeFromDictionary(dictionary, -12);
	removeFromDictionary(dictionary, 43);
	showDictionary(dictionary);

	destroyDictionary(dictionary);

}

