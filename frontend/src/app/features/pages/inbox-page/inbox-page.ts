import { CommonModule } from '@angular/common';
import { Component, computed, ElementRef, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChatResponse } from '../../models/chatResponseDto';
import { signal } from '@angular/core';
import { MessageDto } from '../../models/messageDto';
import { MessageService } from '../../../core/services/message-service';
import { ChatService } from '../../../core/services/chat-service';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-inbox-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './inbox-page.html',
  styleUrl: './inbox-page.css',
})
export class InboxPage {
  chats = signal<ChatResponse[]>([])
  selectedChat = signal<ChatResponse | null>(null)
  messages = signal<MessageDto[]>([])

  currentUserId = '';
  searchText = signal('')
  newMessage = '';

  currentChatId = '';

  @ViewChild('messagesContainer')
  messagesContainer!: ElementRef;

  filteredChats = computed(()=>{
    const search = this.searchText().toLowerCase();

    return this.chats().filter(chat =>
      `${chat.firstName} ${chat.lastName}`
      .toLowerCase()
      .includes(search) ||

      chat.username.toLowerCase().includes(search)
    )
  });
  
  private scrollToBottom(): void{
    setTimeout(()=>{
      const element = this.messagesContainer?.nativeElement;

      if(element) element.scrollTop = element.scrollHeight;
    })
  }

  constructor(private messageService: MessageService, 
    private chatService: ChatService, 
    private route: ActivatedRoute,
    private router: Router
  ){
    
  }

  openProfile(): void{
    
    this.router.navigate(['/profile', this.selectedChat()?.userId])
  }

  ngOnInit(){
    this.currentChatId = this.route.snapshot.paramMap.get('id')!;
    this.currentUserId = localStorage.getItem('userId') || ' ';
    this.loadChats() 
    this.messageService.messageReceived$.subscribe(message => {
      const currentChat = this.selectedChat()

      if(currentChat && message.chatId === currentChat.id){
        this.messages.update(messages => [
          ...messages,
          message
        ]);
      }
      this.scrollToBottom()
    })
    this.scrollToBottom()
  }
  
  loadChats(){
    this.chatService.getAllChats().subscribe({
      next: res=>{
        this.chats.set(res)
        console.log("CURRRENTENTENTNETN")
        console.log(this.currentChatId)
        if(this.currentChatId != ''){
          this.chats().forEach(chat =>{
          if(chat.userId == this.currentChatId) {
            this.selectChat(chat);
            console.log('TEST - =-= -= -= =- =-');
          }
          })
        }
        
        console.log("Chats:")
        console.log(res);
        this.scrollToBottom()
      },
      error: err=>{
        console.error(err)
      }
    })
  }

  selectChat(chat: ChatResponse){
    this.selectedChat.set(chat)

    this.loadMessages(chat.id)
  }

  loadMessages(chatId: string){
    this.messageService.getMessages(chatId).subscribe({
      next: res=>{
        this.messages.set(res);
        this.scrollToBottom()
      },
      error: err=>{
        console.error(err);
      }
    })

  }

  async sendMessage(): Promise<void>{
    const chat = this.selectedChat();

    const content = this.newMessage.trim();

    if(!chat || !content) return;

    try {
      await this.messageService.sendMessage(
        chat.id,
        chat.userId,
        content
      );
      this.newMessage = ''
    } catch(error){
      console.error(error)
    }
  }
}
