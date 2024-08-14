//Assessment 1 
 using System;
 using System.IO;
 using System.Text.RegularExpressions;
 using System.Diagnostics;

public class CaesarCipher{
	//Main method
	public static void Main(){
		bool exceptionB = true;//Used for exception handling
		string cipherText =""; //Stores the Ciphered Text of caesarShiftEncodedText.txt file
			
		//loop if exception detected
		while(exceptionB){
			try{
				//Calls the method Message()
				Message();
				exceptionB = false;
			}
			//Catch an exception if an index goes out of the array
			catch(IndexOutOfRangeException e){
				Console.WriteLine("The exception:{0}",e);
			}
			//Catch an exception if input is the wrong format
			catch(FormatException e){
				Console.WriteLine("The exception:{0}",e);
			}
			//Catch all exceptions
			catch(Exception e){
				Console.WriteLine("The exception:{0}",e);
			}
				
			Console.ReadLine();
			Console.WriteLine("   ");
					
			try{
				// Reads the file caesarShiftEncodedText.txt and stores it in cipherText
				cipherText = File.ReadAllText(@"caesarShiftEncodedText.txt");
				
				//Checks if cipherText is empty
				if (string.IsNullOrEmpty(cipherText)){
					Console.WriteLine("The Text File is empty");
				}
				exceptionB = false;
			}
			//Catch FileNotFoundException
			catch(FileNotFoundException e){
				Console.WriteLine("The exception:{0}",e);
			}
				
			//Displays the file contents of caesarShiftEncodedText.txt 
			Console.WriteLine(cipherText);
			Console.ReadLine();	
			
			try{
				//Calls the method DisplayShifts()
				DisplayShifts(cipherText);
				exceptionB = false;
			}catch(Exception e){
				Console.WriteLine("The exception:{0}",e);
			}
			
			try{
				//Measures the efficiency of my letter frequency method
				Stopwatch lfTimer = Stopwatch.StartNew();
				
				//Calls the method LetterFrequency()
				 CaesarLetterFrequency.LetterFrequency(cipherText);
				
				//Stops the stopwatch timer for letter frequency
				lfTimer.Stop();
				
				//Returns the time efficiency of my letter frequency  method
				Console.WriteLine("Time taken for LetterFrequency: {0}ms", lfTimer.Elapsed.TotalMilliseconds);
				exceptionB = false;
			}catch(IndexOutOfRangeException e){
				Console.WriteLine("The exception:{0}",e);
			}
			catch(FormatException e){
				Console.WriteLine("The exception:{0}",e);
			}
			catch(Exception e){
				Console.WriteLine("The exception:{0}",e);
			}
		
		}//while loop end
		
		Console.ReadLine();
        
	}//End Main Method 
	//Message Method
	private static void Message(){
		string plainMessage;//Stores the plain text message input  from the User
		bool validCheck = false;
		string shiftInput;//Stores the shift input from the User
		string cipherMessage;//Stores the cipher text message created from the plain text message
		int messageShift;//Stores the validated integer shift input
		int counter = 0;//A counter used in loops
		
		//Use a class called Regex and is used  to create a object for regular expression validation
		Regex regularE = new Regex("^[0-9]\\d*$");
				
		do{
					
			validCheck = false;
				
			//Get  plain text message from user
			Console.WriteLine("LETTERS ONLY  AND NO SPACES !! Please enter a message to encrypt:");
			plainMessage = Console.ReadLine();
		
			//Check if  input is empty
			if (string.IsNullOrEmpty(plainMessage)){
				validCheck = true;
			}
			
			//Converts plain text message string into a character Array
			char []plainMessageArray = plainMessage.ToCharArray();
				
			//Checks each Character using ascii if it is  not A - Z or  not a - z 	
			foreach(char character in plainMessageArray){
				
				if ((int)character < 65 || (int)character > 90 && (int)character < 97 || (int)character > 122){
					validCheck = true;
				}
					
			}
		 //loops until validCheck is false
		}while(validCheck == true);
				
		validCheck = false;
				
		do{
					
			do{
				//Get user shift input
				Console.WriteLine("Integer ONLY Please enter the Shift key");
				shiftInput = Console.ReadLine();
						
				//Check if shiftInput matchs the regular expression 
				if(regularE.IsMatch(shiftInput)){
					validCheck = false;
				}
						
				//Convert shiftInput string to a character array
				char[]shiftMessageArray =shiftInput.ToCharArray();
				
				//loop through each character in character array
				foreach(char character in shiftMessageArray){
					//Checks each Character using ascii if it is  not in the range of  0-9
					if ((int)character < 49 || (int)character > 57){
						validCheck = true;
					}
				}
				//Checks if input is a two digit number 	
				if (shiftMessageArray.Length == 2){
					//Check if the first digit is 0 and the second digit is 0-9
					if(shiftMessageArray[0] == 0 &&shiftMessageArray[1] >= 0 && shiftMessageArray[1] <= 9){
						validCheck = false;
					}
					//Check if the first digit is 1 and the second digit is 0-9
					else if(shiftMessageArray[0] == 1 && shiftMessageArray[1] >= 0 && shiftMessageArray[1] <= 9){
						validCheck = false;
					}
					//Check if the first digit is 2 and the second digit is 0-5
					else if(shiftMessageArray[0] == 2 && shiftMessageArray[1] >= 0 && shiftMessageArray[1] <= 5){
						validCheck = false;
					}
					//Check if the first digit is greater than 2 and the second digit is 0-9
					else if(shiftMessageArray[0] > 2 && shiftMessageArray[1] >= 0 && shiftMessageArray[1] <= 9){
						validCheck = true;
					}
				}
				else if (shiftMessageArray.Length > 2){
					validCheck = true;
				}
				
				shiftInput ="";
								
				//Recreates the Shift Input 	
				for(counter = 0;counter< shiftMessageArray.Length;counter++){
					
					//Uses concatination with each character to rebuild string
					shiftInput = shiftInput + shiftMessageArray[counter];
				}		
						
			  //loops until the validCheck false
			}while(validCheck == true);
			
			//Converts the validated string shiftInput into a integer
			messageShift = Convert.ToInt32(shiftInput);
					
			validCheck = true;
			
			//Checks if the shift is between 1 and 25
			if(messageShift >= 1 && messageShift <=25){
				validCheck = false;
			}
		 //loops until valid check is false
		}while(validCheck == true);
				
		//Encrypts the users plain Message and stores it
		cipherMessage = ApplyOrRemoveCipher.EncryptDecrypt(plainMessage,messageShift);
		
		//Display  users Cipher Message
		Console.WriteLine("Your Message  Encrypted : {0}",cipherMessage);
		Console.WriteLine("   ");
			
		//Measures the efficiency of my decryption method using a stopwatch
		Stopwatch dTimer = Stopwatch.StartNew();			
		//Decrypts the users Cipher Message
		plainMessage = ApplyOrRemoveCipher.EncryptDecrypt(cipherMessage,- messageShift);
		//Stop stopwatch
		dTimer.Stop();
		
		//Returns the time efficiency of my decryption method
		Console.WriteLine("Time taken for decryption : {0}ms", dTimer.Elapsed.TotalMilliseconds);
		//Display users Plain Message
		Console.WriteLine("Your Message  Decrypted : {0}",plainMessage);
				
			
	}//End Message Method 	
	//DisplayShifts Method
	public static void  DisplayShifts(string dSCipherText){
		int shift; // Stores the key which shifts the letters left if encrypting and shifts the letters right if decrypting 
		string plainText = "";//Stores the plaintext from the file caesarShiftPlainText.txt
		int correctShift = 0;//Store the correct shift that decrypts the cipher text
		string [] plainTextArray = new string [26];//Stores the plainText of each shift
									
		//  Loops through  and performs each shift 
		for (shift = 0; shift < 26; shift++){
						 
			//Stores the return value from the method that decrypts the cipher text
			plainTextArray[shift] = ApplyOrRemoveCipher.EncryptDecrypt(dSCipherText, - shift);	
															
			//Displays the Decryption Shift for each shift 			
			Console.WriteLine("Decryption Shift(left):{0}",shift);
			
			//Displays the PlainText for each shift 
			Console.WriteLine("Candidate plaintext:\n{0} ", plainTextArray[shift]);		
			Console.WriteLine("   ");
			Console.ReadLine();
						
		} 
						
		correctShift = 5;
																
		//Output the the correct shift that decrypts the text and display the decrypted message				
		Console.WriteLine("The Decrypted shift is {0}",correctShift);
		Console.WriteLine("Candidate plaintext:\n{0} ", plainTextArray[correctShift]);
		Console.WriteLine("   ");
				
		try{
			//Write to a file 	
			File.WriteAllText( @"caesarShiftPlainText.txt",plainTextArray[correctShift]);
		
		}catch(FileNotFoundException E){
			Console.WriteLine("The exception:{0}",E);
		}
							
		//Reads the text from caesarShiftPlainText.txt and stores it in plainText
		try{
			plainText = File.ReadAllText(@"caesarShiftPlainText.txt");
		}catch(FileNotFoundException E){
			Console.WriteLine("The exception:{0}",E);
		}
		Console.WriteLine("Text read from caesarShiftPlainText.txt:");
		//Displays the  contents of caesarShiftPlainText.txt 
		Console.WriteLine(plainText);
		Console.ReadLine();							
	}
}
class ApplyOrRemoveCipher {
	//Caesar Encryption and Decryption Method
	public static string EncryptDecrypt(string cText,int cShift ){
		int ascii; //Store the ascii of a char
		int counter = 0; //Used as a counter in a loop
		char[] charInText = cText.ToCharArray();
		
		// go through each letter  in the array 
		for( counter = 0; counter < charInText.Length;counter++){
			//Converts a character which is stored in the charintext  into  its ascii equivalant
			ascii = (int)charInText[counter];
			
			//Checks to make sure that the character ascii values are between A's ascii value  and Z's ascii value
			if (ascii >= 65 && ascii <= 90){
				//Applies the shift which moves the ascii values of a character
				ascii += cShift;
				//Checks to make sure that the ascii values do not go under 65
				if(ascii < 65 ){
					//If under 65 then add 26 to the ascii value  
					ascii += 26;
											
				}else if( ascii > 90){
					//If over 90 then substract 26 from the ascii value
					ascii -=26;
				}
			} 
			//Checks to make sure that the character ascii values are between a's ascii value and z's ascii value
			else if (ascii >= 97 && ascii <= 122){
				//Applies the shift which moves the ascii values of a character
				ascii += cShift;
				//Checks to make sure that the ascii values do not go under 97
				if(ascii < 97 ){
					//If under 97 then add 26 to the ascii value  
					ascii += 26;
												
				}
				//Checks to make sure that the ascii values do not go over 122
				else if( ascii > 122){
					//If over 122 then substract 26 from the ascii value
					ascii -=26;
				}
			}
			//Creates the Text from the decryption or encryption by converting the ascii value back into a char and using concatination
			charInText[counter] = (char)ascii;
		}
		return new string(charInText);
	}
}
class CaesarLetterFrequency{
	public  static void LetterFrequency(string lfCipherText){
		double[]letterCounter = new double[26];//Holds the letter frequency count for each letter
		char[]alphabet = new char[26];//Holds each letter of the Alphabet
		double[] percentFrequency = new double[26];//Holds the letter frequency percentage for each letter in the text
		int lfShift = 0;//Store the shift that decrypts the cipher text
		string lfPlainText = "";//Stores the plaintext from the file caesarShiftPlainText.txt
		int highestFrequency = 0;//Contains the highest frequency to find E
		int highestFrequencyCounter = 0;//Counter for highest frequency
		int count = 0;//Counter
				
		//Generates the alphabet and stores it in a array
		for( count = 0; count <26; count++){
			alphabet[count]= (char)(count +65);
		}	
		
		//Converts the lFCipherText string into a character array
		char[] lfCharinText = lfCipherText.ToCharArray();
				
		//Goes throught each character in the array
		foreach(char character in lfCharinText){
			//Searches alphabet and gets occurance of each character in the text
			for( count = 0; count < 26; count++){
				if(character == alphabet[count]){
					letterCounter[count]++;
				}
			}
		}
		//Displays frequency of each letter
		for(count= 0;count <26; count++){
			Console.WriteLine("Letter:{0}  Frequency: {1}",alphabet[count],letterCounter[count]);
			//Calculates the percentage frequency
			percentFrequency[count]= ((letterCounter[count]/lfCharinText.Length)*100);
			Console.WriteLine("Letter:{0} Frequency:{1}",alphabet[count],percentFrequency[count]);
		}
		
		Console.WriteLine("   ");
		Console.ReadLine();		
		//Gets the letter with the highest frequency in the text
		for(count = 0;count < letterCounter.Length; count++){
			if (highestFrequency < (int)letterCounter[count]){
				highestFrequency = (int)letterCounter[count];
				highestFrequencyCounter = count;
			}
		}
		//Displays the letter with the highest frequency
		Console.WriteLine("Highest Frequency Letter:{0} Frequency:{1}",alphabet[highestFrequencyCounter],percentFrequency[highestFrequencyCounter]);
		
		//Checks the difference  between the letter of the highest frequency and E
		if((int)alphabet[highestFrequencyCounter] > (int)alphabet[4]){
			
			lfShift =(int)alphabet[highestFrequencyCounter] - (int) alphabet[4];
					
		}else if ((int)alphabet[highestFrequencyCounter] < (int)alphabet[4]){
			
			lfShift =(int) alphabet[4]-(int)alphabet[highestFrequencyCounter];
			
		}
				
		//Displays the shift obtained by letter frequency
		Console.WriteLine("The correct shift is :{0}",lfShift);
		
		//Use the shift to decrypt the cipher text
		lfPlainText = ApplyOrRemoveCipher.EncryptDecrypt(lfCipherText, - lfShift);
		
		//Display the decrypted text
		Console.WriteLine(lfPlainText);
			
	}
}
		

	

	