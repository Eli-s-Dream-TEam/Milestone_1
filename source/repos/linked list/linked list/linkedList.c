#include <stdio.h>
#include <string.h>


int isSubstring(char* s1, char* s2) {
	unsigned int m = strlen(s1);
	unsigned int n = strlen(s2);
	// A loop to slide pat[] one by one
	for (int i = 0; i <= n - m; i++) {
		int j;

		// check for pattern match
		for (j = 0; j < m; j++)
			if (s2[i + j] != s1[j])
				break;

		if (j == m)
			return i;
	}

	return -1;
}


void creatFile(char* srcFile, char* dstFile) {
	char buffer[2];
	FILE* inputStream;
	FILE* outputStream;
	inputStream = fopen_s(srcFile, "r");
	outputStream = fopen_s(dstFile, "w");
	while (fread(&buffer, sizeof(char), 2, inputStream) > 0) {
		fwrite(buffer, sizeof(char), 2, outputStream);
	}
	fclose(inputStream);
	fclose(outputStream);

}

void creatFile2(char* srcFile, char* dstFile, char* srcOs, char* dstOs, int swapF) {
	int be = 0;
	char* mac = "-mac";
	char* unx = "-unix";
	char* win = "-win";
	char buffer[2];
	unsigned char BOM[2];

	FILE* inputStream = fopen_s(srcFile, "r");
	FILE* outputStream = fopen_s(dstFile, "w");

	fread(BOM, sizeof(char), 2, inputStream);

	if (BOM[0] == 0xFE && BOM[1] == 0xFF) {
		be = 1;
	}

	while (fread(buffer, sizeof(char), 2, inputStream) > 0) {

		// win to mac
		if (strcmp(srcOs, win) == 0 && strcmp(dstOs, mac) == 0) {
			if (buffer[be] == '\r') {
				if (swapF == 1) {
					char temp = buffer[0];
					buffer[0] = buffer[1];
					buffer[1] = temp;
				}
				fwrite(buffer, sizeof(char), 2, outputStream);
			}
			fread(buffer, sizeof(char), 2, inputStream);
			if (buffer[be] == '\n') {
				continue;
			}
		}

		// win to unix
		else if (strcmp(srcOs, win) == 0 && strcmp(dstOs, unx) == 0) {
			if (buffer[be] == '\r') {
				buffer[be] = '\n';
				if (swapF == 1) {
					char temp = buffer[0];
					buffer[0] = buffer[1];
					buffer[1] = temp;
				}
				fwrite(buffer, sizeof(char), 2, outputStream);
				fread(buffer, sizeof(char), 2, inputStream);
				if (buffer[be] == '\n') {
					continue;
				}
			}

		}


		// unix to win
		else if (strcmp(srcOs, unx) == 0 && strcmp(dstOs, win) == 0) {
			if (buffer[be] == '\n') {
				buffer[be] = '\r';
				if (swapF == 1) {
					char temp = buffer[0];
					buffer[0] = buffer[1];
					buffer[1] = temp;
				}
				fwrite(buffer, sizeof(char), 2, outputStream);
				buffer[be] = '\n';
			}

		}


		// unix to mac
		else if (strcmp(srcOs, unx) == 0 && strcmp(dstOs, mac) == 0) {
			if (buffer[be] == '\n') {
				buffer[be] = '\r';
			}
		}


		// mac to unix
		else if (strcmp(srcOs, mac) == 0 && strcmp(dstOs, unx) == 0) {
			if (buffer[be] == '\r') {
				buffer[be] = '\n';
			}
		}

		// mac to win
		else if (strcmp(srcOs, mac) == 0 && strcmp(dstOs, win) == 0) {
			if (buffer[be] == '\r') {
				if (swapF == 1) {
					char temp = buffer[0];
					buffer[0] = buffer[1];
					buffer[1] = temp;
				}
				fwrite(buffer, sizeof(char), 2, outputStream);
				buffer[be] = '\n';
			}
		}

		if (swapF == 1) {
			char temp = buffer[0];
			buffer[0] = buffer[1];
			buffer[1] = temp;
		}

		fwrite(buffer, sizeof(char), 2, outputStream);

	}
	fclose(inputStream);
	fclose(outputStream);

}


int main(int argc, char* argv[]) {

	int size = argc;
	if (size < 4 || size > 7) {
		return 1;
	}

	char* srcFileName = argv[2];
	char* dstFileName = argv[3];


	if (!isSubstring(".txt", srcFileName) || !isSubstring(".txt", dstFileName)) {
		return 0;
	}

	if (size == 4) {
		creatFile(srcFileName, dstFileName);
		return 0;
	}

	if (size < 6) {
		return 1;
	}

	char* srcOs = argv[4];
	char* dstOs = argv[5];

	char* swap = "-swap";
	int swapFlag = 0;
	if (strcmp(swap, argv[6]) == 0) {
		swapFlag = 1;
	}

	creatFile2(srcFileName, dstFileName, srcOs, dstOs, swapFlag);

	return 0;
}
