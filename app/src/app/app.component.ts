import { CommonModule } from '@angular/common'
import { Component, ElementRef, ViewChild } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { RouterOutlet } from '@angular/router'

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  	title = 'aws-chat-app'
  	messages: { sender: string, text: string }[] = []
  	newMessage: string = ''

	
	@ViewChild('chatMessages') private chatMessagesContainer!: ElementRef

	constructor() {
		this.messages.push({ sender: 'bot', text: 'Hello tout le monde.' })
	}

	sendMessage() {
		if (this.newMessage.trim()) 
		{
			this.messages.push({ sender: 'user', text: this.newMessage })
			this.newMessage = ''

			setTimeout(() => {
				this.messages.push({ sender: 'bot', text: 'I received your message!' })
			}, 1000)
		}
	}

	ngAfterViewChecked() {
		this.scrollToBottom()
	}

	private scrollToBottom(): void {
		try {
			this.chatMessagesContainer.nativeElement.scrollTop = this.chatMessagesContainer.nativeElement.scrollHeight
		} catch (err) {
			console.error('Error scrolling to bottom:', err)
		}
	}
}
